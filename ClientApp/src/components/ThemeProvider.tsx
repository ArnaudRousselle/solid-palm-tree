import { PropsWithChildren, useState } from "react";
import { ThemeContext } from "../contexts";

interface IProps extends PropsWithChildren{
    render: ()
}

export const ThemeProvider = ({children}: IProps) => {
  const [mode, setMode] = useState<"light" | "dark">("dark");
  return (
    <ThemeContext.Provider value={{ mode, setMode }}>{children}</ThemeContext.Provider>
  );
};
