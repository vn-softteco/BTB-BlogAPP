import { API_ENDPOINTS } from '@/utils/constants'
import { API } from '@/utils/api'
import { AddOrUpdateCommentFormType } from '@/types'

const addComment = async (data: AddOrUpdateCommentFormType) => {
    return await API.post(API_ENDPOINTS.ADD_COMMENT,  data )
}

const updateComment = async (data: AddOrUpdateCommentFormType) => {
    return await API.put(API_ENDPOINTS.UPDATE_COMMENT,  data )
}

const deleteComment = async (id: string) => {
    return await API.delete(API_ENDPOINTS.DELETE_COMMENT_BY_ID(id) )
}

export default {
    addComment,
    deleteComment,
    updateComment
}