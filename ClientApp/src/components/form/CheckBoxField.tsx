import { Control, FieldPath, useController } from "react-hook-form";
import { Rules } from "./types";

interface IProps<FormState extends {}> extends Rules<FormState> {
  control: Control<FormState>;
  fieldPath: FieldPath<FormState>;
  label: string;
}

export const CheckBoxField = <FormState extends {}>({
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
    <fieldset>
      <label htmlFor={field.name}>
        <input
          id={field.name}
          type="checkbox"
          checked={field.value}
          role="switch"
          {...field}
        />
        {label}
      </label>
    </fieldset>
  );
};
