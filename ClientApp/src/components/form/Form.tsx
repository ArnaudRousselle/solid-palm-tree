import { DeepPartial, useForm } from "react-hook-form";
import { RenderArgs } from "./types";

interface IProps<FormState extends {}> {
  initialValues: DeepPartial<FormState>;
  render: (args: RenderArgs<FormState>) => JSX.Element;
  onSubmit: (values: FormState) => void;
}

export const Form = <FormState extends {}>({
  initialValues,
  render,
  onSubmit,
}: IProps<FormState>) => {
  const { control, handleSubmit, getValues } = useForm<FormState>({
    defaultValues: initialValues,
    mode: "onBlur",
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {render({ control, getValues })}
    </form>
  );
};
