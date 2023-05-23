import { useContext } from "react";
import { UseQueryOptions, useQuery } from "react-query";
import { ApiContext } from "../contexts";
import { PortfolioItem, PortfolioItemArrayRequestResult } from "../api";
import { useQueryWithEvents } from "./useBaseQuery";

export function usePortfoliosQuery(
  options?: Omit<
    UseQueryOptions<unknown, unknown, PortfolioItemArrayRequestResult>,
    "queryKey" | "queryFn"
  >
) {
  const { portfolioApi } = useContext(ApiContext);

  const query = useQuery(
    "portfolios",
    () => portfolioApi.apiPortfolioGetPortfoliosGet(),
    options
  );

  const result = useQueryWithEvents<PortfolioItem[]>(
    query,
    "PortfoliosProjectionArgs"
  );

  return result;
}
