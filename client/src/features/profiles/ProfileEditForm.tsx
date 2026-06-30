import { Button } from '@mui/material'
import { Box } from '@mui/system'
import { useEffect } from 'react'
import TextInput from '../../app/shared/components/TextInput'
import { useForm } from 'react-hook-form'
import { editProfileSchema, type EditProfileSchema } from '../../lib/schemas/editProfileSchema'
import { zodResolver } from '@hookform/resolvers/zod'
import { useProfile } from '../../lib/hooks/useProfile'
import { useAccount } from '../../lib/hooks/useAccount'

type Props = {
    handleEdit: () => void
}

export default function ProfileEditForm(props: Props) {
    const { currentUser } = useAccount();
    const { editProfile, profile } = useProfile(currentUser?.id);

    const { control, handleSubmit, reset } = useForm<EditProfileSchema>({
        resolver: zodResolver(editProfileSchema),
        mode: "onTouched"
    })

    useEffect(() => {
        reset({
            ...profile
        })
    }, [profile]);

    const onSubmit = async (data: EditProfileSchema) => {
        try {
            await editProfile.mutateAsync({ ...data });
        } catch (error) {
            console.log(error);
        }
        console.log(data);
        props.handleEdit();
    }
    return (
        <Box sx={{ borderRadius: 3, p: 3 }} bgcolor={'#777'}>
            <Box component="form" gap={3} display="flex" flexDirection={"column"} onSubmit={handleSubmit(onSubmit)}>
                <TextInput label="Display Name" control={control} name="displayName" />
                <TextInput label="Bio" control={control} name="bio" multiline rows={3} />
                <Box display="flex" justifyContent="end" sx={{ pt: 2 }}>
                    <Button type="submit" color="primary" variant="contained" sx={{ px: 2 }} >
                        Submit
                    </Button>
                </Box>

            </Box>

        </Box>
    )
}
