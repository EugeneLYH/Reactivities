import { Button, Paper, Typography } from "@mui/material";
import { Box } from "@mui/system";
import TestInput from "../../../app/shared/components/TestInput";
import { activitySchema, type ActivitySchema } from "../../../lib/schemas/activitySchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import SelectTestInput from "../../../app/shared/components/SelectTestInput";
import { categoryOptions } from "./categoryOptions";


export default function ExampleForm() {
    const {control, handleSubmit} = useForm<ActivitySchema>({
        mode: "onTouched",
        resolver: zodResolver(activitySchema)
    })
    const onSubmit = (data: ActivitySchema) => {
        console.log(data)
    }

    return (
        <Paper sx={{ p: 3 }}>
            <Box component={"form"} display="flex" flexDirection={"column"} gap={3} onSubmit={handleSubmit(onSubmit)}>
                <Typography variant="h6">Example Form</Typography>
                <TestInput label="Title" name="title" control={control} />
                <TestInput label="Description" name="description" control={control} multiline rows={3} />
                <SelectTestInput label="Category" name="category" control={control} items={categoryOptions} />
                <Box display="flex" justifyContent={"end"}>
                    <Button type='submit' variant="contained" color="success">Submit</Button>
                </Box>
                
            </Box>

        </Paper>
    )
}
