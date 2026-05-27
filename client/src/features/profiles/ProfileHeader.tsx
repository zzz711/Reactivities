import {
  Avatar,
  Box,
  Button,
  Chip,
  Divider,
  Grid,
  Paper,
  Stack,
  Typography,
} from '@mui/material';
import { useParams } from 'react-router';
import { useProfile } from '../../lib/hooks/useProfile';

export default function ProfileHeader() {
  const { id } = useParams();
  const { isCurrentUser, profile, updateFollowing } = useProfile(id);

  if (!profile) return null;

  return (
    <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
      <Grid container spacing={2}>
        <Grid size={8}>
          <Stack direction="row" spacing={3} sx={{ alignItems: 'center' }}>
            <Avatar
              src={profile.imageUrl}
              alt={profile.displayName + ' avatar'}
              sx={{ width: 150, height: 150 }}
            />
            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              <Typography variant="h4">{profile.displayName}</Typography>
              {profile.isFollowing && (
                <Chip
                  variant="outlined"
                  color="secondary"
                  label="Following"
                  sx={{ borderRadius: 1 }}
                />
              )}
            </Box>
          </Stack>
        </Grid>
        <Grid size={4}>
          <Stack spacing={2} sx={{ alignItems: 'center' }}>
            <Box
              sx={{
                display: 'flex',
                justifyContent: 'space-around',
                width: '100%',
              }}
            >
              <Box sx={{ textAlign: 'center' }}>
                <Typography variant="h6">Followers</Typography>
                <Typography variant="h2">{profile.followersCount}</Typography>
              </Box>
              <Box sx={{ textAlign: 'center' }}>
                <Typography variant="h6">Following</Typography>
                <Typography variant="h2">{profile.followingCount}</Typography>
              </Box>
            </Box>
            {!isCurrentUser && (
              <>
                <Divider sx={{ width: '100%' }} />
                <Button
                  onClick={() => updateFollowing.mutate()}
                  disabled={updateFollowing.isPending}
                  fullWidth
                  variant="outlined"
                  color={profile.isFollowing ? 'error' : 'success'}
                >
                  {profile.isFollowing ? 'Unfollow' : 'Follow'}
                </Button>
              </>
            )}
          </Stack>
        </Grid>
      </Grid>
    </Paper>
  );
}
