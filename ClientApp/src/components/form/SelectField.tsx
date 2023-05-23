import { Control, FieldPath, useController } from "react-hook-form";
import { Rules } from "./types";

interface IProps<FormState extends {}> extends Rules<FormState> {
  control: Control<FormState>;
  fieldPath: FieldPath<FormState>;
  items: { label: string; value: string | number }[];
  label: string;
}

export const SelectField = <FormState extends {}>({
  control,
  fieldPath,
  items,
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
      <select id={field.name} {...field}>
        {field.value === "" && (
          <option value="" disabled={true}>
            Veuillez sélectionner...
          </option>
        )}
        {items.map((item, index) => (
          <option key={item.value + "_" + index} value={item.value}>
            {item.label}
          </option>
        ))}
      </select>
    </label>
  );
};
