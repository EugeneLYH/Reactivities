import { FormControl, FormHelperText, InputLabel, MenuItem, Select, type SelectProps } from "@mui/material"
import { useController, type FieldValues, type UseControllerProps } from "react-hook-form"

type Props<T extends FieldValues> = {
    items: { text: string, value: string }[]
} & UseControllerProps<T> & SelectProps

export default function SelectTestInput<T extends FieldValues>(props: Props<T>) {
    const { field, fieldState } = useController({ ...props });
    return (
        <FormControl error={!!fieldState.error}>
            <InputLabel>{props.label}</InputLabel>
            <Select label={props.label} value={field.value || ''} onChange={field.onChange}>
                {props.items.map(item => (
                    <MenuItem key={item.value} value={item.value}>{item.text}</MenuItem>
                ))}
            </Select>
            <FormHelperText>{fieldState.error?.message}</FormHelperText>
        </FormControl>
    )
}
