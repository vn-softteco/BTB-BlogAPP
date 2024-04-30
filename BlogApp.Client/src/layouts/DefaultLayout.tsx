import Header from '@/components/Header/Header'

export const DefaultLayout = ( props: { children: JSX.Element | null } ): JSX.Element => (
    <>
        <Header />
        {props.children}
    </>
);
