import { createContext } from "react";

export interface IThemeContext {
  mode: "light" | "dark";
  setMode: (mode: "light" | "dark") => void;
}

export const ThemeContext = createContext<IThemeContext>({
  mode: "dark",
  setMode: () => {},
});

