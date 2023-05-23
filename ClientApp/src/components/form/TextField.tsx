import { Control, FieldPath, useController } from "react-hook-form";
import { Rules } from "./types";

interface IProps<FormState extends {}> extends Rules<FormState> {
  control: Control<FormState>;
  fieldPath: FieldPath<FormState>;
  label: string;
}

export const TextField = <FormState extends {}>({
  control,
  fieldPath,
  label,
  ...rules
}: IProps<FormState>) => {
  const {
    field,
    fieldState: { error },
  } = useController({
    control,
    name: fieldPath,
    rules,
  });

  return (
    <label htmlFor={field.name}>
      {label}
      <input
        id={field.name}
        type="text"
        {...field}
        aria-invalid={!!error ? true : undefined}
      />
      {Boolean(error?.message) && <small style={{textAlign: "right", color: "var(--primary)"}}>{error?.message}</small>}
    </label>
  );
};
