import { Control, RegisterOptions, UseFormGetValues } from "react-hook-form";

export interface RenderArgs<FormState extends {}> {
  control: Control<FormState>;
  getValues: UseFormGetValues<FormState>;
}

export type Rules<FormState extends {}> = Omit<
  RegisterOptions<FormState>,
  "valueAsNumber" | "valueAsDate" | "setValueAs" | "disabled"
>;
