export type TUser = {
    id: string
    firstName: string
    lastName: string
    email: string
}

export type BlogPostListView = {
  id: string
  title: string
  content: string
  creationDate: string
  createdByFullName: string
}

export type BlogPostDetails = {
  id: string
  title: string
  content: string
  creationDate: string
  createdByFullName: string
  comments: Comment[],
  handleBlogPostDelete: (id: string) => void
}

export type Comment = {
  id: string
  text: string
  creationDate: string
  createdByFullName: string
}

export type AddCommentType = {
  blogPostId: string
  text: string
}

export interface ApiResponse<T> {
  errorMessage: string | null
  success: boolean
  data: T
}

export type ApiError = {
  key: string,
  name: string[]
}