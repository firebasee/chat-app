import { ChatClientComponent } from "@/components/chat-client-component";

export default async function ChatRoomPage({ params }: { params: { id: string } }) {
  const { id: chatRoomId } = await params;

  return <ChatClientComponent chatRoomId={chatRoomId} />;
}
