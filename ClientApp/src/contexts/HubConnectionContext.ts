import { createContext } from "react";
import { HubConnection } from "@microsoft/signalr";

export interface IHubConnectionContext {
  hubConnection: HubConnection | null;
}

export const HubConnectionContext = createContext<IHubConnectionContext>({
  hubConnection: null,
});
