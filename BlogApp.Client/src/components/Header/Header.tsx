import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import { Styles } from '@/types'

const styles: Styles = {
    mainContainer: {
      flexGrow: 1,
      display: 'flex',  
    },
    toolbar: {
        justifyContent: 'flex-end',  
    },
    block: {
      width: '100%',
      height: '42px',
      display: 'block',
      borderRadius: '12px',
    },
  }

const Header = (): JSX.Element => {

  return (
    <Box sx={styles.mainContainer}>
      <AppBar >
        <Toolbar sx={styles.toolbar}>

          <Button color="inherit">Login</Button>
        </Toolbar>
      </AppBar>
    </Box>
  );
}

export default Header;