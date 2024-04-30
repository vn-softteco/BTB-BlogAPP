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
import { useNavigate } from 'react-router-dom'
import { ROUTES } from '@/utils/constants'

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

type CommentDetailProps = {
    comment: Comment,
    blogPostId: string,
    handleCommentDelete: (id: string) => void
}

const CommentDetails = (props: CommentDetailProps): JSX.Element => {
    const navigate = useNavigate()

    return (
        <Box sx={styles.mainbox}>
            <Card sx={styles.card}>
                <CardHeader title={props.comment.createdByFullName} />        
                <CardContent>  
                    <Box>{props.comment.text}</Box>
                </CardContent>
                <CardActions sx={styles.actionButton}>
                    <Button
                        variant='contained'
                        color='error'
                        fullWidth                
                        onClick={() => props.handleCommentDelete(props.comment.id)}
                    >
                        Delete
                    </Button>
                    <Button
                        variant="contained"
                        fullWidth
                        color='secondary'     
                        onClick={() => navigate(ROUTES.ROUTE_UPDATE_COMMENT(props.blogPostId), {state : { id: props.comment.id, text: props.comment.text}})}
                    >
                        Edit
                    </Button>
                </CardActions>
            </Card>
        </Box>
    );
  }

  export default CommentDetails