"use client";

import { useEffect, useState } from "react";
import { ChatRoomCard } from "@/components/chat-room-card";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

interface ChatRoom {
  id: string;
  name: string;
}

export default function Home() {
  const [chatRooms, setChatRooms] = useState<ChatRoom[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [newRoomName, setNewRoomName] = useState("");
  const [creatingRoom, setCreatingRoom] = useState(false);

  const fetchChatRooms = async () => {
    try {
      setLoading(true);
      const response = await fetch("/api/chatrooms");
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data: ChatRoom[] = await response.json();
      setChatRooms(data);
    } catch (e: any) {
      setError(e.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchChatRooms();
  }, []);

  const handleCreateRoom = async () => {
    if (!newRoomName.trim()) return;

    setCreatingRoom(true);
    try {
      const response = await fetch("/api/chatrooms", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name: newRoomName }),
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      setNewRoomName("");
      fetchChatRooms(); // Refresh the list of chat rooms
    } catch (e: any) {
      setError(e.message);
    } finally {
      setCreatingRoom(false);
    }
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-screen">Loading chat rooms...</div>
    );
  }

  if (error) {
    return (
      <div className="flex justify-center items-center min-h-screen text-red-500">
        Error: {error}
      </div>
    );
  }

  return (
    <main className="flex min-h-screen flex-col items-center p-24">
      <h1 className="text-4xl font-bold mb-8">Chat Rooms</h1>

      <Card className="w-full max-w-md mb-8">
        <CardHeader>
          <CardTitle>Create New Chat Room</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="flex space-x-2">
            <Input
              type="text"
              placeholder="Enter room name"
              value={newRoomName}
              onChange={(e) => setNewRoomName(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === "Enter") {
                  handleCreateRoom();
                }
              }}
              disabled={creatingRoom}
            />
            <Button onClick={handleCreateRoom} disabled={creatingRoom || !newRoomName.trim()}>
              {creatingRoom ? "Creating..." : "Create"}
            </Button>
          </div>
        </CardContent>
      </Card>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {chatRooms.length > 0 ? (
          chatRooms.map((room) => <ChatRoomCard key={room.id} id={room.id} name={room.name} />)
        ) : (
          <p>No chat rooms available. Create one above!</p>
        )}
      </div>
    </main>
  );
}
