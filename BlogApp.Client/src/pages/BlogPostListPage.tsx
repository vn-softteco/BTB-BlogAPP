import { useState, useEffect } from 'react'
import { BlogPostService } from '@/services'
import { useQueries } from '@tanstack/react-query'
import { BlogPostListView } from '@/types'
import { Box, Grid, Button, CircularProgress } from '@mui/material'
import { BlogPostListItemCard } from '@/components/BlogPost'
import { useNavigate } from 'react-router-dom'
import { ROUTES } from '@/utils/constants'
import { DefaultLayout } from '@/layouts/DefaultLayout'

type BlogPostsPageData = {
    blogPosts: BlogPostListView[]
}

import { Styles } from '@/types'

const styles: Styles = {
  mainBox: {
    mt: 15,
    display: 'flex',
    flexDirection: 'column'
  },
  cardsBox: {
    maxWidth: '80%',
    alignSelf: 'center',
  },
  addButton: {
    mb: 5,
    display: 'flex',
    alignSelf: 'center',
    justifyContent: 'end',
    mt: 3
  },
  grid: {
    gap: '2.66666%'
  },
  itemCard: {
    width: '23%',
    mt: 1,
  }
}

const BlogPostsPage = (): JSX.Element => {
  const navigate = useNavigate()
  const [loading, setLoading] = useState(true);
  const [blogPostsData, setBlogPostsData] = useState<BlogPostsPageData>({
      blogPosts: []
  })    

  const [
      { data: allBlogPosts, isLoading: isBlogPostsLoaded },
    ] = useQueries({
      queries: [
        {
          queryKey: [BlogPostService.getAllBlogPosts.name],
          queryFn: () => BlogPostService.getAllBlogPosts(),
        },
      ],
    })
    

  useEffect(() => {
      setLoading(isBlogPostsLoaded)
      if (allBlogPosts?.length) {
          setBlogPostsData({blogPosts: allBlogPosts})
      }
  }, [allBlogPosts])

  return (
    <DefaultLayout>
      <Box sx={styles.mainBox}>
        <Box sx={styles.cardsBox} >
          { loading ?
            <CircularProgress />
            :
            <>
              <Grid sx={styles.grid} container spacing={1.5}>
                { blogPostsData.blogPosts.length !== 0 ?
                  <>
                    { 
                      blogPostsData.blogPosts.map((blogPost: BlogPostListView) => (
                        <Box key={blogPost.id} sx={styles.itemCard}>
                          <BlogPostListItemCard {...blogPost}></BlogPostListItemCard>
                        </Box>)
                      )
                    }
                  </>
                  : 
                  <h1>No BlogPosts</h1>
                }
              </Grid>
              <Box sx={styles.addButton}>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={() => navigate(ROUTES.ADD_BLOGPOST)}>
                    Add Post
                </Button>
              </Box>
            </>           
          }
        </Box>
        
      </Box>
    </DefaultLayout>
  );
}

export default BlogPostsPage