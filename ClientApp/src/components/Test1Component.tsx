import {
  TextField,
  Form,
  SubmitButton,
  NumberField,
  DateField,
  now,
  CheckBoxField,
  SelectField,
} from "./form";

type FormState = {
  name: string;
  firstName: string;
  age: number;
  date: string;
  active: boolean;
  choice: string;
};

const initialValues: FormState = {
  name: "",
  firstName: "",
  age: 20,
  date: now(),
  active: true,
  choice: "",
};

export const Test1Component = () => {
  return (
    <Form
      initialValues={initialValues}
      onSubmit={(values) => console.log("submit", values)}
      render={({ control, getValues }) => (
        <>
          <TextField
            label="Prénom"
            control={control}
            fieldPath="firstName"
          />
          <NumberField
            label="Age"
            control={control}
            fieldPath="age"
            validate={() => {
              console.log("je valide");
              const age = getValues("age");
              return age > 20;
            }}
          />
          <DateField label="Date" control={control} fieldPath="date" />
          <CheckBoxField label="Actif" control={control} fieldPath="active" />
          <SelectField
            label="Catégorie"
            control={control}
            fieldPath="choice"
            items={[
              { label: "A", value: "aaa" },
              { label: "B", value: "bbb" },
            ]}
          />
          <SubmitButton />
        </>
      )}
    />
  );
};
