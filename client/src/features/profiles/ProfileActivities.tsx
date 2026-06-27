import { Card, CardActionArea, CardContent, CardMedia, Grid, Tab, Tabs, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useProfile } from "../../lib/hooks/useProfile";
import { Link, useParams } from "react-router";
import { useState, type SyntheticEvent } from "react";
import { formatDate } from "../../lib/types/util/util";

const tabContent = [
    { label: "Future Events", filter: 'future' },
    { label: "Past Events", filter: 'past' },
    { label: "Hosting", filter: 'hosting' },
];
export default function ProfileActivities() {
    const { id } = useParams();
    const [value, setValue] = useState(0);
    const { activities } = useProfile(id, undefined, tabContent[value].filter);
    console.log(activities);

    const handleChange = (_: SyntheticEvent, value: number) => {
        setValue(value);
    }

    return (
        <Box display='flex' flexDirection='column' gap={2}>
            <Box display='flex'>
                <Tabs
                    sx={{
                        borderBottom: 1,
                        borderColor: 'divider'
                    }}
                    value={value}
                    onChange={handleChange}
                >
                    {tabContent.map((filter, index) => (
                        <Tab key={index} label={filter.label} />
                    ))}
                </Tabs>
            </Box>
            <Grid container spacing={3}>
                {activities?.map(activity => (
                    <Grid size={3} key={activity.id}>
                        <Card>
                            <CardActionArea component={Link} to={`/activities/${activity.id}`}>
                                <CardMedia
                                    component="img"
                                    height="75"
                                    image={`/images/categoryImages/${activity.category}.jpg`}
                                    alt={`${activity.category} image`}
                                />
                                <CardContent>
                                    <Box display='flex' justifyContent='center'>
                                        <Typography variant="body1" sx={{ fontWeight: 'bold' }}>{activity.title}</Typography>

                                    </Box>
                                    <Box display='flex' justifyContent='center'>
                                        <Typography variant="body2">{formatDate(activity.date)}</Typography>
                                    </Box>
                                </CardContent>
                            </CardActionArea>

                        </Card>
                    </Grid>

                ))}

            </Grid>
        </Box>
    )
}
