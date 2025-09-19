'use client';

import { useState, useEffect, useRef } from 'react';
import { useChatSignalR } from '@/hooks/useChatSignalR';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';

interface Message {
  id: string;
  userName: string;
  text: string;
  timestamp: string;
}

interface ChatClientComponentProps {
  chatRoomId: string;
}

export function ChatClientComponent({ chatRoomId }: ChatClientComponentProps) {
  const [messageInput, setMessageInput] = useState('');
  const [userName, setUserName] = useState('Guest'); // Placeholder for username
  const { messages, sendMessage, isConnected } = useChatSignalR(chatRoomId);
  const messagesEndRef = useRef<HTMLDivElement>(null);

  const handleSendMessage = () => {
    if (messageInput.trim() && userName.trim()) {
      sendMessage(userName, messageInput);
      setMessageInput('');
    }
  };

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  return (
    <div className="flex flex-col h-screen bg-gray-100 dark:bg-gray-900">
      <Card className="flex flex-col flex-grow m-4">
        <CardHeader className="border-b">
          <CardTitle className="text-2xl">Chat Room: {chatRoomId}</CardTitle>
          <p className="text-sm text-gray-500 dark:text-gray-400">
            {isConnected ? "Connected" : "Connecting..."}
          </p>
        </CardHeader>
        <CardContent className="flex-grow p-4 overflow-hidden">
          <ScrollArea className="h-full pr-4">
            <div className="space-y-4">
              {messages.length === 0 ? (
                <p className="text-center text-gray-500 dark:text-gray-400">No messages yet. Start the conversation!</p>
              ) : (
                messages.map((msg, index) => (
                  <div key={msg.id || index} className="flex items-start space-x-3">
                    <div className="flex-shrink-0">
                      {/* You can add an avatar here */}
                    </div>
                    <div className="flex-grow">
                      <div className="flex items-baseline space-x-2">
                        <span className="font-semibold text-gray-900 dark:text-gray-100">{msg.userName}</span>
                        <span className="text-xs text-gray-500 dark:text-gray-400">{new Date(msg.timestamp).toLocaleTimeString()}</span>
                      </div>
                      <p className="text-gray-800 dark:text-gray-200">{msg.text}</p>
                    </div>
                  </div>
                ))
              )}
              <div ref={messagesEndRef} />
            </div>
          </ScrollArea>
        </CardContent>
        <CardFooter className="border-t p-4">
          <div className="flex w-full space-x-2">
            <Input
              type="text"
              placeholder="Your name" 
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              className="w-1/4"
            />
            <Input
              type="text"
              placeholder="Type your message..."
              value={messageInput}
              onChange={(e) => setMessageInput(e.target.value)}
              onKeyPress={(e) => {
                if (e.key === 'Enter') {
                  handleSendMessage();
                }
              }}
              className="flex-grow"
              disabled={!isConnected}
            />
            <Button onClick={handleSendMessage} disabled={!isConnected || !messageInput.trim()}>
              Send
            </Button>
          </div>
        </CardFooter>
      </Card>
    </div>
  );
}
