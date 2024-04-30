import {
    Card,
    CardHeader,
    CardContent,
    CardActions,
    Box,
    Button 
  } from '@mui/material'
import { Comment } from '@/types'
import { Styles } from '@/types'

const styles: Styles = {
    mainbox: {
        display: 'flex',
        justifyContent: 'center',
    },
    card: {
        width: '80%'
    },
    info: {
        display: 'flex',
        justifyContent: 'end',
        mt: 1,
        mr: 2
    },
    actionButton: {
        display: 'flex',
        justifyContent: 'end',
        mr: 2
    }
}

const CommentDetails = (comment: Comment): JSX.Element => {
  
    return (
        <Box sx={styles.mainbox}>
            <Card sx={styles.card}>
                <CardHeader title={comment.createdByFullName} />        
                <CardContent>  
                    <Box>{comment.text}</Box>
                </CardContent>
                <CardActions sx={styles.actionButton}>
                    <Button
                        variant='contained'
                        color='error'                    
                        onClick={() => comment.handleCommentDelete(comment.id)}
                    >
                        Delete
                    </Button>
                </CardActions>
            </Card>
        </Box>
    );
  }

  export default CommentDetails