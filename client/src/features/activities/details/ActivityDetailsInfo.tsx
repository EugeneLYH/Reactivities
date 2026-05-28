
import { CalendarToday, Info, Place } from "@mui/icons-material";
import { Button, Divider, Grid, Paper, Typography } from "@mui/material";
import { formatDate } from "../../../lib/types/util/util";
import type { Activity } from "../../../lib/types";
import MapComponent from "../../../app/shared/components/MapComponent";
import { Box } from "@mui/system";
import { useState } from "react";

type Props = {
    activity: Activity
}
export default function ActivityDetailsInfo({ activity }: Props) {
    const [mapOpen, setMapOpen] = useState(false);
    return (
        <Paper sx={{ mb: 2 }}>

            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <Info color="info" fontSize="large" />
                </Grid>
                <Grid size={11}>
                    <Typography>{activity.description}</Typography>
                </Grid>
            </Grid>
            <Divider />
            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <CalendarToday color="info" fontSize="large" />
                </Grid>
                <Grid size={11}>
                    <Typography>{formatDate(activity.date)}</Typography>
                </Grid>
            </Grid>
            <Divider />

            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <Place color="info" fontSize="large" />
                </Grid>
                <Grid size={11} display="flex" justifyContent={"space-between"}>
                    <Typography>
                        {activity.venue}, {activity.city}
                    </Typography>
                    <Button onClick={() => setMapOpen(!mapOpen)}>
                        {mapOpen ? 'Hide map' : 'Open map'}
                    </Button>
                </Grid>
            </Grid>
            {mapOpen && (
                <Box sx={{ height: 400, zIndex: 1000, display: 'block' }}>
                    <MapComponent
                        position={[activity.latitude, activity.longitude]}
                        venue={activity.venue}
                    />
                </Box>
            )}
        </Paper>
    )
}
