import { useNavigate } from 'react-router-dom'

import {
    Card,
    CardHeader,
    CardContent,
    CardActions,
    Box,
    Button,
    Grid
  } from '@mui/material'
import { ROUTES } from '@/utils/constants'
import { BlogPostDetails, Comment } from '@/types'
import { Styles } from '@/types'
import { red } from '@mui/material/colors'
import CommentDetails from '@/components/Comment/CommentsDetails'

const styles: Styles = {
    mainbox: {
        display: 'flex',
        flexDirection: 'column',
        mt: 10,
        mb: 5
    },
    blogpost: {
        display: 'inline',
        width: '80%',
        alignSelf: 'center',
    },
    info: {
        display: 'flex',
        justifyContent: 'end',
        mt: 1,
        mr: 2
    },
    infotext: {
        color: red
    },
    actionButton: {
        display: 'flex',
        justifyContent: 'end',
        mr: 2
    },
    commentGrid: {
        mt: 10,
        width: '80%',
        flexDirection: 'column',
        display: 'flex',
        alignSelf: 'center',
    },
}

const BlogPostDetailsComponent = (blogPost: BlogPostDetails): JSX.Element => {
    const navigate = useNavigate()
  
    return (
        <Box sx={styles.mainbox}>
            <Card sx={styles.blogpost}>
                <CardHeader title={blogPost.title} />        
                <CardContent>  
                    <Box>{blogPost.content}</Box>
                    <Box sx={styles.info}>
                        <Box sx={styles.infotext}>{blogPost.createdByFullName}</Box>
                    </Box>
                </CardContent>
                <CardActions sx={styles.actionButton}>
                    <Button
                        variant="contained"                        
                        onClick={() => navigate(ROUTES.POST_ADD_COMMENT(blogPost.id))}
                    >
                        Add comment
                    </Button>
                </CardActions>
            </Card>
            <Grid sx={styles.commentGrid} container spacing={1.5}>
                {blogPost.comments.map((comment: Comment) => (
                    <Box sx={styles.itemCard}>
                        <CommentDetails {...comment}></CommentDetails>
                    </Box>
                ))}
            </Grid>
        </Box>
    );
  }

  export default BlogPostDetailsComponent