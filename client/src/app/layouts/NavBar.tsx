import {
  Box,
  AppBar,
  Toolbar,
  Typography,
  Button,
  Container,
} from '@mui/material';
import { Group } from '@mui/icons-material';

type Props = {
  openForm: () => void;
}

export default function NavBar(props: Props) {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            'linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)',
        }}
      >
        <Container>
          <Toolbar sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Box>
              <Button sx={{ display: 'flex', gap: 2 }}>
                <Group fontSize="large" />
                <Typography variant="h4">Reactivities</Typography>
              </Button>
            </Box>
            <Box sx={{ display: 'flex' }}>
                <Button
                  sx={{
                    fontSize: '1.2rem',
                    textTransform: 'uppercase',
                    fontWeight: 'bold',             
                  }}                  
                >
                  Activities
                </Button>
                <Button
                  sx={{
                    fontSize: '1.2rem',
                    textTransform: 'uppercase',
                    fontWeight: 'bold',
                  }}
                >
                  About
                </Button>
                <Button
                  sx={{
                    fontSize: '1.2rem',
                    textTransform: 'uppercase',
                    fontWeight: 'bold',
                  }}
                >
                  Contact
                </Button>
            </Box>
            <Button size="large" variant="contained" color="warning" onClick={props.openForm}>
              Create activity
            </Button>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
