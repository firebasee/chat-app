import { NextResponse } from "next/server";

const BACKEND_BASE_URL = process.env.NEXT_PUBLIC_BACKEND_BASE_URL || "http://localhost:5145";

export async function GET(request: Request, { params }: { params: { id: string } }) {
  const { id } = params; // chatRoomId

  try {
    const response = await fetch(`${BACKEND_BASE_URL}/api/rooms/${id}/messages`);

    if (!response.ok) {
      const errorText = await response.text();
      return new NextResponse(errorText, { status: response.status });
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error: any) {
    console.error(`Error proxying historical messages for room ${id}:`, error);
    return new NextResponse(`Failed to fetch historical messages: ${error.message}`, {
      status: 500,
    });
  }
}
