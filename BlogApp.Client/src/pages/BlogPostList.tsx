import { useState, useEffect } from 'react'
import { BlogPostService } from '@/services'
import { useQueries } from '@tanstack/react-query'
import { BlogPostListView } from '@/types'
import { Box, Grid, Button } from '@mui/material'
import { BlogPostListItemCard } from '@/components/BlogPost'
import { useNavigate } from 'react-router-dom'
import { ROUTES } from '@/utils/constants'

type BlogPostsPageData = {
    blogPosts: BlogPostListView[]
}

import { Styles } from '@/types'

const styles: Styles = {
  mainBox: {
      display: 'flex',
      flexDirection: 'column'
  },
  cardsBox: {
    maxWidth: '80%',
    alignSelf: 'center',
  },
  addButton: {
    width: '80%',
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
      if (allBlogPosts?.length) {
          setBlogPostsData({blogPosts: allBlogPosts})
      }
  }, [allBlogPosts])

  return (
    <Box sx={styles.mainBox}>
      <Box sx={styles.cardsBox} >
          <Grid sx={styles.grid} container spacing={1.5}>
            {blogPostsData.blogPosts.map((blogPost: BlogPostListView) => (
              <Box sx={styles.itemCard}>
                <BlogPostListItemCard {...blogPost}></BlogPostListItemCard>
              </Box>
            ))}
          </Grid>
      </Box>
      <Box sx={styles.addButton}>
        <Button
          variant="contained"
          color="primary"
          onClick={() => navigate(ROUTES.ADD_BLOGPOST)}>
            Add Post
        </Button>
      </Box>
    </Box>
  );
}

export default BlogPostsPage