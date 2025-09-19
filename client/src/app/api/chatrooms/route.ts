import { NextResponse } from "next/server";

const BACKEND_BASE_URL = process.env.NEXT_PUBLIC_BACKEND_BASE_URL || "http://localhost:5145";

export async function GET() {
  try {
    const response = await fetch(`${BACKEND_BASE_URL}/api/chatrooms`);

    if (!response.ok) {
      const errorText = await response.text();
      return new NextResponse(errorText, { status: response.status });
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error: any) {
    console.error("Error proxying chatrooms GET request:", error);
    return new NextResponse(`Failed to fetch chatrooms: ${error.message}`, { status: 500 });
  }
}

export async function POST(request: Request) {
  try {
    const body = await request.json();
    const response = await fetch(`${BACKEND_BASE_URL}/api/chatrooms`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });

    if (!response.ok) {
      const errorText = await response.text();
      return new NextResponse(errorText, { status: response.status });
    }

    const data = await response.json();
    return NextResponse.json(data, { status: 201 });
  } catch (error: any) {
    console.error("Error proxying chatrooms POST request:", error);
    return new NextResponse(`Failed to create chatroom: ${error.message}`, { status: 500 });
  }
}
