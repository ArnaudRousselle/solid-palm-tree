import { useState } from "react";
import { QueryClient, QueryClientProvider } from "react-query";
import { Configuration, PortfolioApi } from "./api";
import "./App.css";
import { Test1Component, ThemeSwitch } from "./components";
import { ApiContext, IApiContext } from "./contexts/ApiContext";
import { useHubConnection } from "./hooks";
import { HubConnectionContext } from "./contexts";
import { ThemeProvider } from "./components/ThemeProvider";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
    },
  },
});

function App() {
  const hubConnection = useHubConnection();

  const [apiContext] = useState<IApiContext>({
    portfolioApi: new PortfolioApi(
      new Configuration({ basePath: "http://localhost:5016" })
    ), //todo: à changer
  });

  return (
    <HubConnectionContext.Provider value={{ hubConnection }}>
      <QueryClientProvider client={queryClient}>
        <ApiContext.Provider value={apiContext}>
          <ThemeProvider>
            <ThemeSwitch />
            <Test1Component />
          </ThemeProvider>
        </ApiContext.Provider>
      </QueryClientProvider>
    </HubConnectionContext.Provider>
  );
}

export default App;
