import { PropsWithChildren } from "react";
import { HubConnectionContext } from "../contexts";
import { useHubConnection } from "../hooks";

interface IProps extends PropsWithChildren {}

export const HubConnectionContextProvider = ({ children }: IProps) => {
  const hubConnection = useHubConnection();

  return (
    <HubConnectionContext.Provider value={{ hubConnection }}>
      {children}
    </HubConnectionContext.Provider>
  );
};
