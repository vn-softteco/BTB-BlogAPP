import { useState, useEffect } from 'react'
import { BlogPostService } from '@/services'
import { useQueries } from '@tanstack/react-query'
import { BlogPostDetails } from '@/types'
import { Box } from '@mui/material'
import { BlogPostDetailsComponent } from '@/components/BlogPost'
import { useParams } from 'react-router'
import { useNavigate } from 'react-router-dom'
import { ROUTES } from '@/utils/constants'


const BlogPostsPage = (): JSX.Element => {

    const navigate = useNavigate()
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
            setBlogPostData(blogPostWithComments)
        }
    }, [blogPostWithComments])

    const handleBlogPostDelete = async(id: string) => {
        const res = await BlogPostService.deleteBlogPost(id);
        
        if (res.status === 200 && res.data.success) {
            navigate(ROUTES.GET_BLOGPOSTs, { replace: true })        
        }
    }

    return (
        <Box >
            { blogPostsDetailsData && 
                <Box>                    
                    <BlogPostDetailsComponent {...blogPostsDetailsData} handleBlogPostDelete={handleBlogPostDelete}></BlogPostDetailsComponent>
                </Box>
            }
        </Box>
        
    );
}

export default BlogPostsPage