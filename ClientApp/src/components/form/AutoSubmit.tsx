import { useEffect, useRef } from "react";
import { Control, useWatch } from "react-hook-form";

interface IProps<FormState extends {}> {
  control: Control<FormState>;
}

export const AutoSubmit = <FormState extends {}>({
  control,
}: IProps<FormState>) => {
  const values = useWatch({control});
  const buttonRef = useRef<HTMLButtonElement>(null);

  useEffect(() => {
    if (!buttonRef.current) return;
    buttonRef.current.click();
  }, [values]);

  return <button ref={buttonRef} style={{ visibility: "collapse" }} />;
};
