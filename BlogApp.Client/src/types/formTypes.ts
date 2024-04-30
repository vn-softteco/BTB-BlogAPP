export type SignInFormType = {
    email: string,
    password: string
}

export type SignUpAccountFormType = {
    email: string,
    password: string,
    firstName: string,
    lastName: string,
}

export type AddOrUpdateBlogPostFormType = {
    id?: string,
    title: string,
    content: string
}

export type AddBlogPostFormType = {
    title: string,
    content: string
}

export type UpdateBlogPostFormType = {
    id?: string,
    title: string,
    content: string
}

export type AddCommentFormType = {
    blogPostId: string,
    text: string
}

export type UpdateCommentFormType = {
    id: string
    blogPostId: string,
    text: string
}

export type AddOrUpdateCommentFormType = {
    id?: string,
    blogPostId: string,
    text: string
}