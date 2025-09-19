import Link from "next/link";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

interface ChatRoomCardProps {
  id: string;
  name: string;
}

export function ChatRoomCard({ id, name }: ChatRoomCardProps) {
  return (
    <Link href={`/chat/${id}`}>
      <Card className="w-[300px] hover:shadow-lg transition-shadow duration-200">
        <CardHeader>
          <CardTitle>{name}</CardTitle>
        </CardHeader>
        <CardContent>
          <p>Join this chat room</p>
        </CardContent>
      </Card>
    </Link>
  );
}
