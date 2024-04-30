import { API_ENDPOINTS } from '@/utils/constants'
import { API } from '@/utils/api'
import { BlogPostListView, BlogPostDetails, ApiResponse, AddBlogPostFormType } from '@/types'

const getAllBlogPosts = async () => {
    const response =  await API.get<ApiResponse<BlogPostListView[]>>(API_ENDPOINTS.GET_ALL_BLOG_POSTS)
    return response.data.data
}

const getBlogPostById = async (id: string): Promise<BlogPostDetails> => {
    const response =  await API.get<ApiResponse<BlogPostDetails>>(API_ENDPOINTS.GET_ALL_BLOG_POST_BY_ID(id))
    return response.data.data
}

const addBlogPost = async (data: AddBlogPostFormType) => {
    return await API.post(API_ENDPOINTS.ADD_BLOG_POST,  data )
}

const deleteBlogPost = async (id: string) => {
    return await API.delete(API_ENDPOINTS.DELETE_BLOG_POST_BY_ID(id))
}

export default {
    getAllBlogPosts,
    getBlogPostById,
    addBlogPost,
    deleteBlogPost
}