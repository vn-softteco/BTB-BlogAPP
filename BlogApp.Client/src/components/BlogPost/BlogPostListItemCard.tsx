import { useNavigate } from 'react-router-dom'
import {
  Card,
  CardHeader,
  CardContent,
  CardActions,
  Button,
  Box,
} from '@mui/material'

import { ROUTES } from '@/utils/constants'
import { BlogPostListView } from '@/types'

import { Styles } from '@/types'

const styles: Styles = {
  content: {
    display: 'block',
    lineClamp: '10',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    height: '250px'
  },
}

const BlogPostListItemCard = (blogPost: BlogPostListView): JSX.Element => {
    const navigate = useNavigate()
  
    return (
      <Card >
        <CardHeader title={blogPost.title} />
  
        <CardContent >  
          <Box sx={styles.content}>{blogPost.content}</Box>
        </CardContent>
  
        <CardActions>
          <Button
            variant="contained"
            fullWidth
            onClick={() => navigate(ROUTES.GET_BLOGPOST_BY_ID(blogPost.id))}
          >
            Details
          </Button>
        </CardActions>
      </Card>
    );
  }

  export default BlogPostListItemCard