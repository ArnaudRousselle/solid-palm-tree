import { PropsWithChildren } from "react";
import { QueryClient, QueryClientProvider } from "react-query";

interface IProps extends PropsWithChildren {}

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
    },
  },
});

export const QueryClientContextProvider = ({ children }: IProps) => {
  return (
    <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
  );
};
