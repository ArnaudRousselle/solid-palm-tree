import { PropsWithChildren, useEffect, useMemo, useState } from "react";
import { IColors, ThemeContext } from "../contexts";

interface IProps extends PropsWithChildren {}

export const ThemeProvider = ({ children }: IProps) => {
  const [mode, setMode] = useState<"light" | "dark">("dark");

  const colors = useMemo(() => getColors(mode), [mode]);

  useEffect(() => {
    const elements = document.getElementsByTagName("html");
    console.log("Ici", elements);
    if (elements.length !== 1) return;
    elements[0].setAttribute("data-theme", mode);
  }, [mode]);

  return (
    <ThemeContext.Provider value={{ mode, colors, setMode }}>
      {children}
    </ThemeContext.Provider>
  );
};

function getColors(mode: "light" | "dark"): IColors {
  switch (mode) {
    case "dark":
      return { error: "#990000" };
    case "light":
      return { error: "#E00000" };
  }
}
