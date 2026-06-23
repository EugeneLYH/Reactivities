import { Grid } from "@mui/system";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { useParams } from "react-router";
import { useProfile } from "../../lib/hooks/useProfile";
import { Divider, Typography } from "@mui/material";

export default function ProfilePage() {
  const { id } = useParams();
  const { loadingProfile } = useProfile(id);

  if (loadingProfile) return <Typography>Loading...</Typography>


  return (
    <Grid>
      <Grid size={12}>
        <ProfileHeader />
        <ProfileContent />
      </Grid>
      <Divider sx={{width: '100%'}} />
    </Grid>
  )
}
