import { createContext } from "react";
import { PortfolioApi } from "../api";

export interface IApiContext {
  portfolioApi: PortfolioApi;
}

export const ApiContext = createContext<IApiContext>({} as IApiContext);
