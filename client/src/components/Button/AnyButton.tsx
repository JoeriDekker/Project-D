type Props = {
    link: string;
    text: string;
}

function AnyButton(props: Props) {
    return (
        <a href={props.link} className="ring-offset-background mx-14 focus-visible:ring-ring flex h-10 w-50 items-center justify-center whitespace-nowrap rounded-md bg-black px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-black/70 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50" type="submit">{props.text}</a>
    )
}

function AnyButton2(props: Props) {
    return (
        <a href={props.link} className="ring-offset-background mx-5 mt-8 focus-visible:ring-ring flex h-10 w-fit items-center justify-center whitespace-nowrap rounded-md bg-secondaryCol px-8 py-2 text-sm font-medium text-black transition-colors hover:bg-secondaryCol/70 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50" type="submit">{props.text}</a>
    )
}

export default AnyButton
export {
    AnyButton2
}