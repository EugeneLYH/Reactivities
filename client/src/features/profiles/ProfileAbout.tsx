import { useState } from 'react'
import { useParams } from 'react-router'
import { useProfile } from '../../lib/hooks/useProfile';
import { Box } from '@mui/system';
import { Button, Divider, Typography } from '@mui/material';
import ProfileEditForm from './ProfileEditForm';

export default function ProfileAbout() {
    const { id } = useParams();
    const { profile } = useProfile(id);
    const [editMode, setEditMode] = useState(false);
    

    const handleEdit = () => {
        setEditMode(!editMode);
    }
    return (
        <Box>
            <Box display='flex' justifyContent={'space-between'}>
                <Typography variant='h5'>About {profile?.displayName}</Typography>
                <Button
                    onClick={handleEdit}
                >
                    {editMode ? "Cancel " : "Edit Profile"}
                </Button>
            </Box>
            <Divider sx={{ my: 2 }} />
            <Box sx={{ overflow: 'auto', maxHeight: 350 }}>
                {editMode ?
                    (
                        <ProfileEditForm handleEdit={handleEdit} />
                    ) : (
                        <Typography variant='body1' sx={{ whiteSpace: 'pre-wrap' }}>
                            {profile?.bio || 'No description added'}
                        </Typography>
                    )

                }

            </Box>
        </Box >
    )
}
