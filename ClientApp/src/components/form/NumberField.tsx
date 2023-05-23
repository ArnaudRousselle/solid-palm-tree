import { Control, FieldPath, useController } from "react-hook-form";
import { Rules } from "./types";
import { useMemo, useState } from "react";

interface IProps<FormState extends {}> extends Rules<FormState> {
  control: Control<FormState>;
  fieldPath: FieldPath<FormState>;
  label: string;
}

export const NumberField = <FormState extends {}>({
  control,
  fieldPath,
  label,
  ...rules
}: IProps<FormState>) => {
  const {
    field: { name, onChange, value, onBlur },
  } = useController({
    control,
    name: fieldPath,
    rules,
  });
  const [hasFocus, setHasFocus] = useState(false);

  const [inputValue, setInputValue] = useState("");

  const onFieldFocus = () => {
    setInputValue(anyToString(value));
    setHasFocus(true);
  };

  const onFieldBlur = () => {
    setHasFocus(false);
    onBlur();
  };

  const onFieldChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const str = e.target.value;
    const newValue = Number(str);
    setInputValue(str);
    if (!isNaN(newValue)) onChange(newValue);
  };

  const displayedValue = useMemo(
    () => (hasFocus ? inputValue : anyToString(value)),
    [hasFocus, inputValue, value]
  );

  return (
    <label htmlFor={name}>
      {label}
      <input
        id={name}
        name={name}
        type="number"
        onFocus={onFieldFocus}
        onBlur={onFieldBlur}
        onChange={onFieldChange}
        value={displayedValue}
      />
    </label>
  );
};

function anyToString(value: any) {
  const number = Number(value);
  return !isNaN(number) ? number.toString() : "";
}
