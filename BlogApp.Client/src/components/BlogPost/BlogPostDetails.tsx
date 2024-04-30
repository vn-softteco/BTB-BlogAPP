import { useNavigate } from 'react-router-dom'
import { useState, useEffect } from 'react'
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
import { CommentService } from '@/services'

const styles: Styles = {
    mainbox: {
        display: 'flex',
        flexDirection: 'column',
        mt: 10,
        mb: 5,
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
    const [comments, setComments] =
        useState<Comment[]>(blogPost.comments)

    const handleCommentDelete = async(id: string) => {
        const res = await CommentService.deleteComment(id);
        
        if (res.status === 200 && res.data.success) {
            setComments(prevState => (
                prevState.filter(item => item.id !== id)
            ));
        }
    }    
  
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
                        color="error"                    
                        onClick={() => blogPost.handleBlogPostDelete(blogPost.id)}
                    >
                        Delete Blog Post
                    </Button>
                    <Button
                        variant="contained"                  
                        onClick={() => navigate(ROUTES.POST_ADD_COMMENT(blogPost.id))}
                    >
                        Add comment
                    </Button>
                </CardActions>
            </Card>
            <Grid sx={styles.commentGrid} container spacing={1.5}>
                {comments.map((comment: Comment) => (
                    <Box sx={styles.itemCard}>
                        <CommentDetails {...comment} handleCommentDelete={handleCommentDelete}></CommentDetails>
                    </Box>
                ))}
            </Grid>
        </Box>
    );
  }

  export default BlogPostDetailsComponent