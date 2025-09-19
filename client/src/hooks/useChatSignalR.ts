import { useEffect, useState, useRef, useCallback } from "react";
import { HubConnectionBuilder, HubConnection, LogLevel } from "@microsoft/signalr";

interface Message {
  id: string;
  userName: string;
  text: string;
  timestamp: string;
}

export const useChatSignalR = (chatRoomId: string) => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [messages, setMessages] = useState<Message[]>([]);
  const [isConnected, setIsConnected] = useState(false);
  const connectionRef = useRef<HubConnection | null>(null);

  const sendMessage = useCallback(
    async (userName: string, text: string) => {
      if (connectionRef.current && connectionRef.current.state === "Connected") {
        try {
          await connectionRef.current.invoke("SendMessageToRoom", chatRoomId, text);
        } catch (e) {
          console.error("Error sending message:", e);
        }
      } else {
        console.warn("No SignalR connection or connection is not in 'Connected' state.");
      }
    },
    [chatRoomId]
  );

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5145/chatHub")
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    setConnection(newConnection);
    connectionRef.current = newConnection;

    return () => {
      newConnection.stop();
    };
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("SignalR Connected.");
          setIsConnected(true);
          // Join the specific chat room
          connection
            .invoke("JoinRoom", chatRoomId)
            .then(() => console.log(`Joined room: ${chatRoomId}`))
            .catch((err) => console.error("Error joining room:", err));

          // Listen for messages
          connection.on("ReceiveMessage", (message: Message) => {
            setMessages((prevMessages) => [...prevMessages, message]);
          });

          connection.on("ReceiveRoomMessage", (user: string, roomName: string, messageText: string) => {
            // Assuming Message interface can be constructed from these parts
            const newMessage: Message = {
              id: Date.now().toString(), // Temporary ID, ideally from server
              userName: user,
              text: messageText,
              timestamp: new Date().toISOString(),
            };
            setMessages((prevMessages) => [...prevMessages, newMessage]);
          });
        })
        .catch((err) => {
          console.error("Error connecting to SignalR:", err);
          setIsConnected(false);
        });

      connection.onclose((error) => {
        console.log("SignalR connection closed.", error);
        setIsConnected(false);
      });

      connection.onreconnecting((error) => {
        console.log("SignalR reconnecting.", error);
        setIsConnected(false);
      });

      connection.onreconnected((connectionId) => {
        console.log("SignalR reconnected. Connection ID:", connectionId);
        setIsConnected(true);
        // Re-join the room after reconnection
        connection
          .invoke("JoinRoom", chatRoomId)
          .then(() => console.log(`Re-joined room: ${chatRoomId}`))
          .catch((err) => console.error("Error re-joining room:", err));
      });
    }
  }, [connection, chatRoomId]);

  // Function to fetch historical messages (will need a REST API endpoint)
  const fetchHistoricalMessages = useCallback(async () => {
    try {
      const response = await fetch(`/api/chatrooms/${chatRoomId}/messages`); // Assuming this endpoint exists
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data: Message[] = await response.json();
      setMessages(data);
    } catch (e) {
      console.error("Error fetching historical messages:", e);
    }
  }, [chatRoomId]);

  useEffect(() => {
    if (isConnected) {
      fetchHistoricalMessages();
    }
  }, [isConnected, fetchHistoricalMessages]);

  return { messages, sendMessage, isConnected };
};
