import { Control, FieldPath, useController } from "react-hook-form";
import { Rules } from "./types";

interface IProps<FormState extends {}> extends Rules<FormState> {
  control: Control<FormState>;
  fieldPath: FieldPath<FormState>;
  label: string;
}

export const DateField = <FormState extends {}>({
  control,
  fieldPath,
  label,
  ...rules
}: IProps<FormState>) => {
  const { field } = useController({
    control,
    name: fieldPath,
    rules,
  });
  return (
    <label htmlFor={field.name}>
      {label}
      <input id={field.name} type="date" {...field} />
    </label>
  );
};

export function now() {
  const date = new Date();
  const yearStr = date.getFullYear().toString().padStart(4, "0");
  const monthStr = (date.getMonth() + 1).toString().padStart(2, "0");
  const dayStr = date.getDate().toString().padStart(2, "0");
  return yearStr + "-" + monthStr + "-" + dayStr;
}
