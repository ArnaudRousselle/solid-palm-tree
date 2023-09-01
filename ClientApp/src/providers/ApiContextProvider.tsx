import { PropsWithChildren, useRef } from "react";
import { ApiContext, IApiContext } from "../contexts";
import { Configuration, PortfolioApi } from "../api";

interface IProps extends PropsWithChildren {}

export const ApiContextProvider = ({ children }: IProps) => {
  const apiContextRef = useRef<IApiContext>({
    portfolioApi: new PortfolioApi(
      new Configuration({ basePath: "http://localhost:5016" })
    ), //todo: à changer
  });

  return (
    <ApiContext.Provider value={apiContextRef.current}>
      {children}
    </ApiContext.Provider>
  );
};
