import { createContext } from "react";

export type IColors = {
  error: string;
};

export interface IThemeContext {
  mode: "light" | "dark";
  colors: IColors;
  setMode: (mode: "light" | "dark") => void;
}

export const ThemeContext = createContext<IThemeContext>({
  mode: "dark",
  colors: { error: "" },
  setMode: () => {},
});
