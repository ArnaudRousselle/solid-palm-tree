import "./App.css";
import { Test1Component, ThemeSwitch } from "./components";
import {
  ApiContextProvider,
  HubConnectionContextProvider,
  QueryClientContextProvider,
  ThemeContextProvider,
} from "./providers";

const App = () => (
  <HubConnectionContextProvider>
    <QueryClientContextProvider>
      <ApiContextProvider>
        <ThemeContextProvider>
          <ThemeSwitch />
          <Test1Component />
        </ThemeContextProvider>
      </ApiContextProvider>
    </QueryClientContextProvider>
  </HubConnectionContextProvider>
);

export default App;
