import { useContext, useEffect } from "react";
import { ThemeContext } from "../contexts";
import { CheckBoxField, Form, SubmitButton } from "./form";
import { Control, useWatch } from "react-hook-form";

export const ThemeSwitch = () => {
  const { mode, setMode } = useContext(ThemeContext);
  //todo: sauvegarder le thème dans le localStorage
  //todo: mettre en place un autosubmit
  return (
    <Form
      initialValues={{ isDark: mode === "dark" }}
      onSubmit={({ isDark }) => setMode(isDark ? "dark" : "light")}
      render={({ control }) => <FormContent control={control} />}
    />
  );
};

const FormContent = ({
  control,
}: {
  control: Control<{
    isDark: boolean;
  }>;
}) => {
  const isDark = useWatch({ control, name: "isDark" });

  useEffect(() => {}, [isDark]);

  return (
    <>
      <CheckBoxField control={control} fieldPath="isDark" label="" />
      <SubmitButton />
    </>
  );
};
