import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { useEffect, useState } from "react";

export function useHubConnection() {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("projectionsHub")
      .configureLogging(LogLevel.Information)
      .build();

    setConnection(connection);
  }, []);

  useEffect(() => {
    if (!connection) return;

    const startConnection = async () => {
      try {
        await connection.start();
        console.log("SignalR Connected.");
      } catch (err) {
        console.log(err);
        setTimeout(startConnection, 5000);
      }
    };

    connection.onclose(async () => {
      await startConnection();
    });

    startConnection();
  }, [connection]);

  return connection;
}
