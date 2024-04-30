import { useState, useEffect } from 'react'
import { BlogPostService } from '@/services'
import { useQueries } from '@tanstack/react-query'
import { BlogPostDetails } from '@/types'
import { Box } from '@mui/material'
import { BlogPostDetailsComponent } from '@/components/BlogPost'
import { useParams } from 'react-router';


const BlogPostsPage = (): JSX.Element => {
    const [blogPostsDetailsData, setBlogPostData] =
        useState<BlogPostDetails | null>(null)

    const { id } = useParams();

    const [
        { data: blogPostWithComments, isLoading: isBlogPostLoaded },
      ] = useQueries({
        queries: [
          {
            queryKey: [BlogPostService.getBlogPostById.name],
            queryFn: () => !!id && BlogPostService.getBlogPostById(id),
          },
        ],
    })
      
    useEffect(() => {
        if (!!blogPostWithComments) {
            console.log('setBlogPostData')
            console.log(blogPostWithComments)
            setBlogPostData(blogPostWithComments)
        }
    }, [blogPostWithComments])
      

    return (
        <Box >
            { blogPostsDetailsData && 
                <Box>                    
                    <BlogPostDetailsComponent {...blogPostsDetailsData}></BlogPostDetailsComponent>
                </Box>
            }
        </Box>
        
    );
}

export default BlogPostsPage