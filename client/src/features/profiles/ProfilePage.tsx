import { Grid } from "@mui/system";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { useParams } from "react-router";
import { useProfile } from "../../lib/hooks/useProfile";
import { Typography } from "@mui/material";

export default function ProfilePage() {
  const { id } = useParams();
  const { profile, loadingProfile } = useProfile(id);

  if (loadingProfile) return <Typography>Loading...</Typography>


  return (
    <Grid>
      <Grid size={12}>
        <ProfileHeader profile={profile} />
      <ProfileContent />
      </Grid>
    </Grid>
  )
}
