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

export type AddBlogPostFormType = {
    title: string,
    content: string
}

export type AddCommentFormType = {
    text: string
}