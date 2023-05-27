import { useContext } from "react";
import { ThemeContext } from "../contexts";
import { AutoSubmit, CheckBoxField, Form } from "./form";

export const ThemeSwitch = () => {
  const { mode, setMode } = useContext(ThemeContext);
  //todo: sauvegarder le thème dans le localStorage
  //todo: mettre en place un autosubmit
  return (
    <Form
      initialValues={{ isDark: mode === "dark" }}
      onSubmit={({ isDark }) => setMode(isDark ? "dark" : "light")}
      render={({ control }) => (
        <>
          <CheckBoxField control={control} fieldPath="isDark" label="" />
          <AutoSubmit control={control} />
        </>
      )}
    />
  );
};