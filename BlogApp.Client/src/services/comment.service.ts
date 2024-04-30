import { API_ENDPOINTS } from '@/utils/constants'
import { API } from '@/utils/api'
import { AddCommentType } from '@/types'

const addComment = async (data: AddCommentType) => {
    return await API.post(API_ENDPOINTS.ADD_COMMENT,  data )
}

const deleteComment = async (id: string) => {
    return await API.delete(API_ENDPOINTS.DELETE_COMMENT_BY_ID(id) )
}

export default {
    addComment,
    deleteComment
}