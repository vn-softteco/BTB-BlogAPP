import { API_ENDPOINTS } from '@/utils/constants'
import { API } from '@/utils/api'
import { AddCommentType } from '@/types'

const addComment = async (data: AddCommentType) => {
    return await API.post(API_ENDPOINTS.ADD_COMMENT,  data )
}

export default {
    addComment
}